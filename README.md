
Workflow with JS scripts:
All vendor scripts are plugged via vendor.js file wich further will be bundled in one file.
Each specific page has its own scrips that loads with page. Using SystemJs all requred dependecies are loaded for the page.

System.js config:
	- download system.js library
	- if you use ES6 syntax: compile all script with babel gulp task 'gulp babel'
		- also you can use next config to run ES6 without files compilening with babel (but firstly download plugin-babel scripts)
			SystemJS.config({
				map: {
					'plugin-babel': '/system.js-0.20.0/plugin-babel/plugin-babel.js',
					'systemjs-babel-build': '/system.js-0.20.0/plugin-babel/systemjs-babel-browser.js'
				},
				transpiler: 'plugin-babel'
			});
	- config system.js
	- import script on page. E.g.:
		SystemJS.import('/js/pages/home/index.babel.js');

Polyfill (Babel) to emulate a full ES2015+ environment:
	- npm install --save babel-polyfill
	- inlude it as root dependency (require("babel-polyfill"))