var gulp = require('gulp');

module.exports = function(tasks, config){
    tasks.forEach(function(name){
        gulp.task(name, require('./tasks/' + name)(config[name]));
    });
    return gulp;
};