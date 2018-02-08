var buildScript = require('../utils/buildScript.js');

//used for watching for changes and rebuilding
module.exports = function(config) {
    return function(){
        return buildScript(config, true);
    }
}