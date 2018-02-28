import vendor from 'scripts/vendor.js';
const { Vue } = vendor;

let app = new Vue({
    el: '#vueApp',
    data: {
        message: "Hello"
    }
});