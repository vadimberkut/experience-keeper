const fs = require('fs');
const path = require('path');
const exec = require('child_process').exec;

module.exports.processJspmBundleEntry = function (config) {

    // TODO: COnsider use Bundle API - https://github.com/jspm/jspm-cli/blob/master/docs/api.md#bundle-api
    // console.log('processJspmBundleEntry: ', config);

    return new Promise((resolve, reject) => {
        let pageIndexPath = config.entry; // path to chaged file (relative to jspm baseURL)
        // get part of path that corresponds to jspm root folder subfolder
        let jspmBaseURLAbsPath = path.join(config.projectRoot, config.jspmConfig.directories.baseURL);
        if (pageIndexPath.indexOf(jspmBaseURLAbsPath) !== 0) {
            console.error(`Error occured while computing source file path: ${pageIndexPath} - ${jspmBaseURLAbsPath}`);
            reject();
            return;
        }
        let takePreceedingSlash = 1;
        pageIndexPath = pageIndexPath.slice(jspmBaseURLAbsPath.length + takePreceedingSlash);

        // delete bundle
        if (config.fileEvent === "deleted") {
            let bundlePath = getJspmBundlePath(pageIndexPath, config.jspmConfig.directories.baseURL);
            let fullBundlePath = path.join(config.projectRoot, bundlePath);
            deleteFileIfExists(fullBundlePath, (err) => {
                if (err) {
                    console.error(`Unable to delete bundle: ${err}`);
                    reject();
                    return;
                }
                console.log(`Bundle successfully deleted: ${fullBundlePath}`);
                deleteFileIfExists(`${fullBundlePath}.map`);
                resolve();
            });
        }

        // add or update bundle
        if (config.fileEvent === "added" || config.fileEvent === "changed") {

            const vendors = ['scripts/vendor.js']; // exclude vendors that will be bundled in separate bundle

            // modules that will be exluded from bundle
            let arithmetic = vendors.reduce((accum, curr) => { return `${accum} - ${curr}` }, '');
            let bundlePath = getJspmBundlePath(pageIndexPath, config.jspmConfig.directories.baseURL);

            let inject = true;
            let minify = true;
            let mangle = false; // trim names or not
            let flags = `${inject ? '--inject' : ''} ${minify ? '--minify' : ''} ${mangle ? '--mangle' : ''}`;

            let jspmCommand = `jspm bundle ${pageIndexPath} ${arithmetic} ${bundlePath} ${flags}`;
            console.log(`Exec '${jspmCommand}'`);

            exec(jspmCommand, (err, stdout, stderr) => {
                console.log(stdout);
                console.error(stderr);
                if(err) reject(err);
                resolve();
            });
        }
    });
}

module.exports.unbundle = function () {
    let jspmCommand = `jspm unbundle`;

    exec(jspmCommand, (err, stdout, stderr) => {
        console.log(stdout);
        console.error(stderr);
    });
}

function getJspmBundlePath(pageIndexPath, jspmBaseUrl) {
    let preserveDestFolderStructure = false; // if false saves all files directly in dest folder. Otherwise uses file's original folder structure
    let relativePageIndexPath = pageIndexPath; // path relative to project root
    relativePageIndexPath = relativePageIndexPath.split('.')[0]; // remove extension
    if (preserveDestFolderStructure === false) {
        relativePageIndexPath = relativePageIndexPath.split(path.sep).filter((x) => !!x).join('-');
    }
    let bundlePath = path.join(jspmBaseUrl, `jspm-bundles`, `${relativePageIndexPath}.bundle.js`);
    return bundlePath;
}

function deleteFileIfExists(path, cb) {
    if (!cb || typeof cb !== "function") {
        cb = () => { };
    }
    fs.stat(path, (err, stats) => {
        if (err) {
            cb(err);
            return;
        }
        fs.unlink(path, (err) => {
            cb(err);
        });
    });
}