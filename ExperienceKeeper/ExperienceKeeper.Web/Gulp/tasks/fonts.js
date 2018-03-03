var gulp = require('gulp');
var concat = require('gulp-concat');

module.exports = function(config){
    return function(){
        return gulp.src(config.entries)
            //.pipe(concat(config.destFile))
            .pipe(gulp.dest(config.destFolder));
    }
}