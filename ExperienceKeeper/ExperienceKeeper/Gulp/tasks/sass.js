var gulp = require('gulp');
var sass = require('gulp-sass');
var autoprefixer = require('gulp-autoprefixer');
var concat = require('gulp-concat');

module.exports = function(config){
    return function () {
        return gulp.src(config.srcFolder)
            .pipe(sass())
            .pipe(autoprefixer(['last 15 versions', '> 1%', 'ie 8', 'ie 7'], {cascade: true}))
            //.pipe(concat(config.destFile))
            .pipe(gulp.dest(config.destFolder));
    }
}