define(function(require, exports, module) {
    module.exports = {
        requestServer: window.location.hostname == 'localhost' ? 'http://localhost:9203/' : 'http://localhost:9203/',
        requestServerNotAPI: window.location.hostname == 'localhost' ? 'http://localhost:9203/' : 'http://localhost/',
        requestDomain : window.location.hostname == 'localhost' ? 'localhost' : 'localhost',

        //DEPLOYMENT TO DEV
        // requestServer: window.location.hostname == 'localhost' ? 'http://localhost:9203/' : 'http://192.168.129.58:8091/',
        // requestServerNotAPI: window.location.hostname == 'localhost' ? 'http://localhost:9203/' : 'http://192.168.129.58:8091',
        // requestDomain: window.location.hostname == 'localhost' ? 'localhost' : 'localhost',

        //DEPLOYMENT TO CLIENT
        // requestServer: window.location.hostname == 'localhost' ? 'http://10.1.4.251:8071/' : 'http://10.1.4.251:8071/',
        // requestServerNotAPI: window.location.hostname == 'localhost' ? 'http://10.1.4.251:8071/' : 'http://api.rbc.iigf.co.id/',
        // requestDomain: window.location.hostname == 'localhost' ? 'http://api.rbc.iigf.co.id/' : 'http://api.rbc.iigf.co.id/',

        cookieFields: {
            Authorization: 'Authorization',
            userName: 'userName',
            roleName: 'roleName',
            firstName: 'firstName'
        },
        datePickerFormat: 'MMM DD, YYYY',
        datePickerYearFormat: 'YYYY',
        aryLogin: ['login', 'forgot_password', 'reset_password'],

    }
});
