/*
    This file contains all vendor libraries imports.
    If import some module directly into file it will result to big file size
*/

import _ from 'lodash';
import * as moment from 'moment';
import Vue from 'vue';

export default {
    _,
    moment,
    Vue: Vue
}

//const lodash = require('lodash');
//const moment = require('moment')

//module.exports = {
//    lodash,
//    moment
//}