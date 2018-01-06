define(function (require, exports, module) {
	'use strict';
	var Backbone = require('backbone');

	module.exports = Backbone.Collection.extend({
		initialize: function (options) {
			if (this.beforeInitialize) {
				this.beforeInitialize(options);
			}

			this.listenTo(this, 'error', function (model, xhr) {
				var message = xhr && xhr.responseText;
				require(['commonfunction'], function (commonFunction) {
					if (message) {
						commonFunction.responseStatusNot200(message, xhr);
					} else{
						commonFunction.toastrError(xhr);
					}
				});
			});
		}
	});
});