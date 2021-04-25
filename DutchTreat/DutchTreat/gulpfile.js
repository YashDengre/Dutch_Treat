/// <binding AfterBuild='default' />
var gulp = require("gulp");
var uglify = require("gulp-uglify");
var concat = require("gulp-concat");


//minify Javascript: for client side javascript not our angular
function minify() {
    //it is telling that find all files in js and its subfolder with.js extension

    return gulp.src(["wwwroot/js/**/*.js"])
        .pipe(uglify())
        .pipe(concat("dutchtreat.min.js"))
        .pipe(gulp.dest("wwwroot/dist/"));
    //ugligy -  it take file and compress and minify the file.
}


//same we will do for css

function stylesMinify() {
    return gulp.src(["wwwroot/css/**/*.css"])
        .pipe(uglify())
        .pipe(concat("dutchtreat.min.css"))
        .pipe(gulp.dest("wwwroot/dist/"));
}

exports.minify = minify;
exports.stylesMinify = stylesMinify;

//default
//exports.default = minify; // it means  just run the minify / and run the the gulp in command
//exports.default = stylesMinify;
exports.default = gulp.parallel(minify, stylesMinify);
//exports.default = gulp.series(minify, stylesMinify); //execute serially
