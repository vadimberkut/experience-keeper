var gulp = require('gulp');
var connect = require('gulp-connect');  //web server

module.exports = function(config){
    return function(){
        return connect.server(config);
    }
}