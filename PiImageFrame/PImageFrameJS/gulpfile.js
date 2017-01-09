var gulp = require('gulp');
var ts = require('gulp-typescript');
var sourcemaps = require('gulp-sourcemaps');

//Refer's TypeScript Configuration file
var tsProject = ts.createProject('tsconfig.json');

//"setup" - Task to move Angular 2 & related files, bootstrap, Jquery 
//from node_modules to ASP.NET Core 1.0's wwwroot folder 
// All the files will part of libs folder
gulp.task('setup', function (done) {
    gulp.src([
      'node_modules/angular2/bundles/js',
      'node_modules/angular2/bundles/angular2.*.js*',
      'node_modules/angular2/bundles/angular2-polyfills.js',
      'node_modules/angular2/bundles/http.*.js*',
      'node_modules/angular2/bundles/router.*.js*',
      'node_modules/es6-shim/es6-shim.min.js*',
      'node_modules/systemjs/dist/*.*',
      'node_modules/commonjs-require/*.*',
      'node_modules/jquery/dist/jquery.*js',
      'node_modules/bootstrap/dist/js/bootstrap*.js',
      'node_modules/ng2-bootstrap/bundles/**/*.js',
      'node_modules/moment/*.js',
      'node_modules/rxjs/bundles/Rx.js'
    ]).pipe(gulp.dest('./js/'));

    gulp.src([
      'node_modules/bootstrap/dist/css/bootstrap.css'
    ]).pipe(gulp.dest('./css'));
});

var paths = {
    appJavascript: ['**/*.ts', '!node_modules/**/*.*' ]
}

//ts - task to transpile TypeScript files to JavaScript using Gulp-TypeScript 
gulp.task('ts', function (done) {
    var tsResult = gulp.src(paths.appJavascript).pipe(ts(tsProject), undefined, ts.reporter.defaultReporter());
    tsResult.js.pipe(sourcemaps.init());
    tsResult.js.pipe(sourcemaps.write());
    return tsResult.js.pipe(gulp.dest(''));
});

//watch -- Task to watch for any changes under 'setup' and 'ts' tasks
gulp.task('watch', ['watch.ts']);

gulp.task('watch.ts', ['ts'], function () {
    return gulp.watch('app/*.ts', ['ts']);
});

gulp.task('default', ['setup', 'watch']);

//gulp.task('default', ['setup']);