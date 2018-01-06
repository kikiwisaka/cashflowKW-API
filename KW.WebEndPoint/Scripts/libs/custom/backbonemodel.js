define(function (require, exports, module) {
	'use strict';
	require('deep-model');
	
	module.exports = Backbone.DeepModel.extend({
	    idAttribute: 'Id',
		initialize: function () {
			this.listenTo(this, 'error', function (model, xhr) {
				var message = xhr && xhr.responseText;
				require(['commonfunction'], function (commonFunction) {
					if (message) {
						commonFunction.responseStatusNot200(message, xhr);
					} else {
						commonFunction.toastrError(xhr);
					}
				});
			});
		}
	});
});
