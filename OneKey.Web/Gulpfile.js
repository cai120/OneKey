/// <binding BeforeBuild='build' />
'use strict';

var gulp = require('gulp');
var sass = require('gulp-sass')(require('sass'));
var concat = require('gulp-concat');
const { series } = require('gulp');

var paths = {
    js: [
        "./node_modules/jquery/dist/jquery.js",
        "./node_modules/jquery-validation/dist/jquery.validate.js",
        "./node_modules/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.js",
        "./node_modules/bootstrap/dist/js/bootstrap.bundle.js",
    ],
    css: [
        "./node_modules/bootstrap-icons/font/bootstrap-icons.css",
        "./node_modules/bootstrap/dist/css/bootstrap.css",
    ],
    fonts: [
        `./node_modules/bootstrap-icons/font/fonts/bootstrap-icons.woff`,
    ], 
}

function buildJs(callback){
    return gulp.src(['./scripts/OneKey.js', './scripts/*.js'])
        .pipe(concat('site.js'))
        .pipe(gulp.dest('./wwwroot/js'));
    callback();
}

function buildCss(callback){
    return gulp.src('./styles/Main.scss')
        .pipe(sass())
        .pipe(concat('site.css'))
        .pipe(gulp.dest('./wwwroot/css'));
    callback();
}

function buildBundleJs(callback){
    return gulp.src(paths.js)
        .pipe(concat('bundle.js'))
        .pipe(gulp.dest('wwwroot/js'));
    callback();
}

function buildBundleCss(callback){
    return gulp.src(paths.css)
        .pipe(sass())
        .pipe(concat('bundle.css'))
        .pipe(gulp.dest('./wwwroot/css'));
    callback();
}

function buildFonts(cb) {
    return gulp.src(paths.fonts)
        .pipe(gulp.dest('./wwwroot/css/fonts'));
    cb();
}

exports.build = series(buildJs, buildCss, buildBundleJs, buildBundleCss, buildFonts);
exports.buildJs = buildJs;
exports.buildCss = buildCss;
exports.buildBundleJs = buildBundleJs;
exports.buildBundleCss = buildBundleCss;
exports.buildFonts = buildFonts;
