const fs = require('fs');
const path = require('path');
const exec = require('child_process').exec;
const jspmConfig = require('./package.json').jspm;
const dirname = __dirname;

// !NOTICE - use 'folder' instead of './folder' when specifying path

var config = {

    // unused task - browserify uses babelif to preprocess code
    babel: {
        entries: [
            'wwwroot/scripts/**/*.js',
            '!wwwroot/scripts/**/*.babel.js',
            '!wwwroot/scripts/**/*.bundle.js',
            '!wwwroot/scripts/**/vendor.js'
        ],
        destFolder: 'wwwroot/scripts'
    },
    //browserify: {
    //    debug: true,
    //    entries: ['src/index.js'],
    //    development: true,
    //    destFile: 'bundle.js',
    //    destFolder: 'src/build'
    //},
    //watchify: {
    //    debug: true,
    //    entries: ['src/index.js'],
    //    development: true,
    //    destFile: 'bundle.js',
    //    destFolder: 'src/build'
    //},
    //server: {
    //    port: 8080,
    //    //host:
    //    root: ['src'],
    //    livereload: true
    //},
    sass: {
        srcFolder: 'wwwroot/styles/**/*.+(scss|sass)',
        // destFile: 'styles.css',
        destFolder: 'wwwroot/styles'
    },
    'minify-css': {
        entries: [
            'wwwroot/styles/**/*.+(css)',
            '!wwwroot/styles/**/*.min.+(css)' // skip minified files
        ],
        destFolder: 'wwwroot/styles'
    },
    //livereload: {
    //    html: 'src/**/*.+(html|htm)',
    //    js: 'src/build/**/*.js',
    //    jsMaps: 'src/build/**/*.map',
    //    css: 'src/build/**/*.css'
    //},
    //fonts: {
    //    entries: [
    //        "node_modules/react-notifications/lib/fonts/**",
    //        "src/fonts/**/*.ttf"
    //    ],
    //    destFolder: 'src/build/fonts'
    //}
};

var gulp = require('./Gulp')([
    'babel',
    //'browserify',
    //'watchify',
    //'server',
    'sass',
    'minify-css',
    //'fonts'
], config);

var connect = require('gulp-connect'); //web server
var watch = require('gulp-watch'); //watching changes
var del = require('del');

//watching only builded files
//gulp.task('livereload', function() {
//    var entries = [config.livereload.html,config.livereload.js,config.livereload.jsMaps,config.livereload.css];
//    gulp.src(entries)
//        .pipe(watch(entries))
//        .pipe(connect.reload());
//});

//wathing for changes to build
gulp.task('watch', function(){
    gulp.watch(config.sass.srcFolder, ['sass', 'minify-css']);
    gulp.watch(config.babel.entries, ['babel']);
});

//Build
//gulp.task('build', [
//    'sass',
//    'fonts',
//    'browserify'
//]);

//Clean deploy files
//gulp.task('clean', function(){
//    return del([
//        'public/**',
//    ]);
//});

//Prepare deploy - move files
//gulp.task('prepare-deploy', [
//    'build'
//], function(){
//    //move files to public
//    gulp.src(['src/*.html']).pipe(gulp.dest('public'));
//    gulp.src(['src/build/**/*']).pipe(gulp.dest('public/build'));
//    gulp.src(['src/images/**/*']).pipe(gulp.dest('public/images'));
//    gulp.src(['src/favicon.ico']).pipe(gulp.dest('public'));
//});

//Deploy
//gulp.task('deploy', [
//    'clean',
//    'prepare-deploy',
//], function(){
//    //run deploy comands
//});


gulp.task('default', [
    'sass', 
    'minify-css',
    'babel',
    //'fonts',
    //'server', 
    //'livereload',
    //'watchify',
    'watch'
]);


gulp.task('v', () => {
    var chokidarFsWatcher = gulp.watch([
        'wwwroot/scripts/pages/**/index.js'
    ], () => {
        // will be called when the watcher emits a change, add or unlink event
    });
    chokidarFsWatcher.on('change', (data) => {
        // TODO: COnsider use Bundle API - https://github.com/jspm/jspm-cli/blob/master/docs/api.md#bundle-api
        // console.log('on change', data);

        let pageIndexPath = data.path; // path to chaged file (relative to jspm baseURL)
        // get part of path that corresponds to jspm root folder subfolder
        let jspmBaseURLAbsPath = path.join(dirname, jspmConfig.directories.baseURL);
        if (pageIndexPath.indexOf(jspmBaseURLAbsPath) !== 0) {
            console.error(`Error occured while computing source file path: ${pageIndexPath} ${jspmBaseURLAbsPath}`);
            return;
        }
        let takePreceedingSlash = 1;
        pageIndexPath = pageIndexPath.slice(jspmBaseURLAbsPath.length + takePreceedingSlash);

        // delete bundle
        if (data.type === "deleted") {
            let bundlePath = getJspmBundlePath(pageIndexPath, jspmConfig.directories.baseURL);
            let fullBundlePath = path.join(dirname, bundlePath);
            deleteFileIfExists(fullBundlePath, (err) => {
                if (err) {
                    console.error(`Unable to delete bundle: ${err}`);
                }
                console.log(`Bunle successfully deleted: ${fullBundlePath}`);
                deleteFileIfExists(`${fullBundlePath}.map`);
            });
            return;
        }

        // add or update bundle
        if (data.type === "added" || data.type === "changed") {

            const vendors = ['scripts/vendor.js']; // exclude vendors that will be bundled in separate bundle

            // modules that will be exluded from bundle
            let arithmetic = vendors.reduce((accum, curr) => { return `${accum} - ${curr}` }, '');
            let bundlePath = getJspmBundlePath(pageIndexPath, jspmConfig.directories.baseURL);

            let inject = true;
            let minify = true;
            let mangle = false; // trim names or not
            let flags = `${inject ? '--inject' : ''} ${minify ? '--minify' : ''} ${mangle ? '--mangle' : ''}`;

            let jspmCommand = `jspm bundle ${pageIndexPath} ${arithmetic} ${bundlePath} ${flags}`;
            console.log(jspmCommand);

            exec(jspmCommand, (err, stdout, stderr) => {
                console.log(stdout);
                console.error(stderr);
            });
        }
    });
});

function getJspmBundlePath(pageIndexPath, jspmBaseUrl) {
    let preserveDestFolderStructure = false; // if false saves all files directly in dest folder. Otherwise uses file's original folder structure
    let relativePageIndexPath = pageIndexPath; // path relative to project root
    relativePageIndexPath = relativePageIndexPath.split('.')[0]; // remove extension
    if (preserveDestFolderStructure === false) {
        relativePageIndexPath = relativePageIndexPath.split(path.sep).filter((x) => !!x).join('-');
    }
    let bundlePath = path.join(jspmBaseUrl, `jspm-bundles`, `${relativePageIndexPath}.bundle.js`);
    return bundlePath;
}

function deleteFileIfExists(path, cb) {
    if (!cb || typeof cb !== "function") {
        cb = () => { };
    }
    fs.stat(path, (err, stats) => {
        if (err) {
            cb(err);
            return;
        }
        fs.unlink(path, (err) => {
            cb(err);
        });
    });
}