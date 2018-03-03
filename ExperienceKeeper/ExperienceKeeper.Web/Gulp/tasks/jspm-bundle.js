const gulp = require('gulp');
const through = require('through2');
const processJspmBundleEntry = require('../utils/jspmBundle.js').processJspmBundleEntry;

/*
    Creates bundle with 'jspm bundle ...'
*/
module.exports = (config) => {
    return () => {
        // console.log(config);
        return gulp.src(config.entries)
                .pipe(through.obj((file, enc, cb) => {
                        let filePath = file.path;
                        processJspmBundleEntry({
                            entry: filePath,
                            jspmConfig: config.jspmConfig,
                            fileEvent: config.fileEvent,
                            projectRoot: config.projectRoot
                        }).then(() => {
                            cb(null, file);
                        }, (err) => {
                            cb(null, file);
                        });
                        // return cb(null, file);
                    }));
    };
}