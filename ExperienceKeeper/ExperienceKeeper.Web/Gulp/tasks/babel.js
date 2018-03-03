const gulp = require("gulp");
const sourcemaps = require("gulp-sourcemaps");
const babel = require("gulp-babel");
const rename = require('gulp-rename');
const uglify = require('gulp-uglify');

// Transforms js to ES5
module.exports = function (config) {
    return function () {
        return gulp.src(config.entries)
            .pipe(sourcemaps.init())
            .pipe(babel())
            .pipe(uglify())
            .pipe(rename({ suffix: '.babel' }))
            .pipe(sourcemaps.write('.'))
            .pipe(gulp.dest(config.destFolder))
    }
}