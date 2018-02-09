const fs = require('fs');
const path = require('path');
const exec = require('child_process').exec;
const jspmConfig = require('./package.json').jspm;
const processJspmBundleEntry = require('./Gulp/utils/jspmBundle.js').processJspmBundleEntry;

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
            'wwwroot/scripts/pages/**/index.js'
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
        
        processJspmBundleEntry({
            entry: data.path,
            jspmConfig: jspmConfig,
            fileEvent: data.type,
            projectRoot: projectRoot
        });
    });
});