/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var sass = require('gulp-sass');
var plumber = require('gulp-plumber');
var notify = require('gulp-notify');

var config = {
    sassFiles: './Styles/*.scss',
    dist: './wwwroot'
};

gulp.task('sass', function () {
    gulp.src(config.sassFiles)
        .pipe(plumber({
            errorHandler: function (error) {
                notify.onError({
                    title: "Gulp error in " + error.plugin,
                    message: error.toString()
                })(error);
            }
        }))
        .pipe(sass())
        .pipe(gulp.dest(config.dist))
});

gulp.task('watch', function () {
    gulp.watch([config.sassFiles], ['sass']);
});
