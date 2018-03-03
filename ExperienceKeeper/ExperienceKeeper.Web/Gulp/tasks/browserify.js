const buildScript = require('../utils/buildScript.js');

const gulp = require("gulp");
const browserify = require('browserify');
const babelify = require('babelify');
const sourcemaps = require("gulp-sourcemaps");
const source = require('vinyl-source-stream'); // Vinyl stream support
const buffer = require('vinyl-buffer');
const gutil = require('gulp-util'); // Provides gulp utilities, including logging and beep
const chalk = require('chalk'); // Allows for coloring for logging

//used to build scripts one time
module.exports = function (config) {

    return () => {
        var  bundler = new browserify({
            entries: [
                './wwwroot/js/site.js',
            ],
            debug: true,
            // defining transforms here will avoid crashing your stream
            transform: [
                babelify
            ]
        })
        //transform(babelify, {
        //    presets: ["es20015"]
        //});
        return bundler.bundle()
            .pipe(source('app.js'))
            .pipe(gulp.dest('./wwwroot/js'));
    };

    //return function(){
    //    var bundler = new browserify({
    //        entries: [
    //            //'./wwwroot/js/pages/home/index.babel.js',
    //            './wwwroot/js/site.babel.js',
    //        ],
    //        debug: true,
    //        // defining transforms here will avoid crashing your stream
    //        // transform: [reactify]
    //    });
    //    return bundler.bundle()
    //        .pipe(source('index_.js'))
    //        .pipe(buffer())
    //        .pipe(sourcemaps.init({ loadMaps: true }))
    //            // Add transformation tasks to the pipeline here.
    //            // .pipe(uglify())
    //            .on('error', mapError)
    //        .pipe(sourcemaps.write('.'))
    //        .pipe(gulp.dest('./wwwroot/js/'));
    //}
}

//Errors
function simpleMapError(err) {
    gutil.log("Browserify Error", gutil.colors.red(err.message));
};

function mapError(err) {
    if (err.fileName) {
        // Regular error
        gutil.log(chalk.red(err.name)
            + ': ' + chalk.yellow(err.fileName.replace(__dirname + '/src/js/', ''))
            + ': ' + 'Line ' + chalk.magenta(err.lineNumber)
            + ' & ' + 'Column ' + chalk.magenta(err.columnNumber || err.column)
            + ': ' + chalk.blue(err.description));
    } else {
        // Browserify error..
        gutil.log(chalk.red(err.name)
            + ': '
            + chalk.yellow(err.message));
    }
}