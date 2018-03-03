const fs = require('fs');
const path = require('path');
const exec = require('child_process').exec;
const jspmConfig = require('./package.json').jspm;
const processJspmBundleEntry = require('./Gulp/utils/jspmBundle.js').processJspmBundleEntry;

const runSequence = require('run-sequence'); // runs gulp tasks sequentially or in parallel

const dirname = __dirname;
const projectRoot = __dirname;

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
    'jspm-bundle': {
        entries: [
            'wwwroot/scripts/pages/**/index.js',
            'wwwroot/scripts/vendor.js',
            'wwwroot/scripts/polyfill.js',
        ],
        jspmConfig: jspmConfig,
        fileEvent: "changed", // use it to update bundles
        projectRoot: projectRoot
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
            '!wwwroot/styles/**/*.min.+(css)', // skip minified files
            '!wwwroot/styles/**/*.+(scss|sass)', // skip sass files
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
    'jspm-bundle',
    'jspm-unbundle',
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


/**
 * Watches changes in page files and regenerates bundle only for that page.
 * !!! - it is not appropriate for heavy developmnet
 */
gulp.task('watch-pages', () => {
    var chokidarFsWatcher = gulp.watch(config['jspm-bundle'].entries, () => {
        // will be called when the watcher emits a change, add or unlink event
    });
    chokidarFsWatcher.on('change', (data) => {
        // TODO: Consider use Bundle API - https://github.com/jspm/jspm-cli/blob/master/docs/api.md#bundle-api
        processJspmBundleEntry({
            entry: data.path,
            jspmConfig: jspmConfig,
            fileEvent: data.type,
            projectRoot: projectRoot
        });
    });
});

//wathing for changes to build
gulp.task('watch', function(){
    gulp.watch(config.sass.srcFolder, ['sass']);
});

//Build
gulp.task('build-styles', (callback) => {
    runSequence(
        'sass',
        'minify-css',
        callback
    );
});
gulp.task('build-scripts', (callback) => {
    runSequence(
        'jspm-unbundle',
        'jspm-bundle',
        callback
    );
});
gulp.task('build', [
   'build-styles',
   'build-scripts'
]);

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
    'build', 
    'watch'
]);

