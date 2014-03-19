$(document).ready(function () {
    $('.slideshow').backstretch([
        "http://localhost:52802/Images/Slides/slide1.jpg",
        "http://localhost:52802/Images/Slides/slide2.jpg",
        "http://localhost:52802/Images/Slides/slide3.jpg",
        "http://localhost:52802/Images/Slides/slide4.jpg",
        "http://localhost:52802/Images/Slides/slide5.jpg"
    ], { duration: 6000, fade: 1500 });
});