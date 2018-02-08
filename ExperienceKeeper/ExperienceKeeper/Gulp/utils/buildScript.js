var gulp = require('gulp');
var browserify = require('browserify'); // Providers "require" support, CommonJS
var watchify = require('watchify'); // Watchify for source changes
var babelify = require('babelify'); // Used to convert ES6 & JSX to ES5
var uglify = require('gulp-uglify'); //compressing
var source = require('vinyl-source-stream'); // Vinyl stream support
var buffer = require('vinyl-buffer');
var sourcemaps = require('gulp-sourcemaps'); // Provide external sourcemap files
var gutil = require('gulp-util'); // Provides gulp utilities, including logging and beep
var chalk = require('chalk'); // Allows for coloring for logging
var notify = require('gulp-notify'); // Provides notification to both the console and Growel
var duration = require('gulp-duration'); // Time aspects of your gulp process
var livereload = require('gulp-livereload'); // Livereload support for the browser

module.exports = function(config, watch){
    var props = {
        entries: config.entries,
        extensions: ['.js', '.jsx'],
        debug : config.debug,
        // debug : config.development,
        transform:  [
            // [babelify, {plugins: ['transform-es2015-spread'], presets: ['es2015', 'react']}], 
            [babelify, {plugins: ['transform-object-rest-spread'], presets: ['es2015', 'react']}], 
        ]
    };

    //livereload.listen(); // Start livereload server

    // watchify() if watch requested, otherwise run browserify() once 
    var bundler = watch ? watchify(browserify(props)) : browserify(props);
    // bundler = bundler.transform(babelify); //don't works
    // bundler = bundler.transform(babelify, {presets: ['es2015']}); //works
    
    bundler.on('update', function(){
        config.debug ? rebundleDev(bundler, config) : rebundleProd(bundler, config);
    });

    return config.debug ? rebundleDev(bundler, config) : rebundleProd(bundler, config);
}

function rebundleDev(bundler, config){
    bundler
    .bundle()
    .on('error', mapError)
    //Pass desired output filename to vinyl-source-stream
    .pipe(source(config.destFile))
    .pipe(buffer()) //uglify expects buffered vinyl file objects
    .pipe(sourcemaps.init({loadMaps: true}))
    .pipe(sourcemaps.write('./maps'))
    // .pipe(uglify())
    .pipe(gulp.dest(config.destFolder))
    .pipe(notify({
        message: 'Generated file: <%= file.relative %>',
    })) // Output the file being created
    .pipe(duration('Javascript bundle time')) // Output time timing of the file creation
    //.pipe(livereload()); // Reload the view in the browser
    ;
}
function rebundleProd(bundler, config){
    bundler
    .bundle()
    .on('error', mapError)
    //Pass desired output filename to vinyl-source-stream
    .pipe(source(config.destFile))
    .pipe(buffer()) //uglify expects buffered vinyl file objects
    .pipe(uglify())
    .pipe(gulp.dest(config.destFolder))
    .pipe(notify({
        message: 'Generated file: <%= file.relative %>',
    })) // Output the file being created
    .pipe(duration('Javascript bundle time')) // Output time timing of the file creation
    //.pipe(livereload()); // Reload the view in the browser
    ;
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