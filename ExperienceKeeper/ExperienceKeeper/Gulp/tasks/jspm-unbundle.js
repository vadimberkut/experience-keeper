const gulp = require('gulp');
const through = require('through2');
const unbundle = require('../utils/jspmBundle.js').unbundle;

/*
    Delete bundles' info from jspm config file
*/
module.exports = () => {
    return () => {
        unbundle();
        return;
    };
}