const gulp = require('gulp');
const cleanCSS = require('gulp-clean-css'); // wrapper for cleanCSS allows to optimize CSS
const rename = require('gulp-rename');
const sourcemaps = require('gulp-sourcemaps');

module.exports = function (config) {
    return function () {
        return gulp.src(config.entries)
            .pipe(rename({ suffix: '.min' }))
            .pipe(sourcemaps.init())
            .pipe(cleanCSS({
                debug: true,
                format: false, // controls output CSS formatting; defaults to false;

            }, (details) => {
                console.log(`${details.name}: ${details.stats.originalSize} -> ${details.stats.minifiedSize} = - ${details.stats.originalSize - details.stats.minifiedSize}`);
                }))
            .pipe(sourcemaps.write())
            .pipe(gulp.dest(config.destFolder));
    }
}