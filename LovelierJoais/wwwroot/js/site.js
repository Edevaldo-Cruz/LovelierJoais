// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const $simpleCarousel = document.querySelector(".js-carousel--simple");

new Glider($simpleCarousel, {
    slidesToShow: 4,
    slidesToScroll: 1,
    draggable: true,
    //dots: ".js-carousel--simple-dots",
    arrows: {
        prev: ".js-carousel--simple-prev",
        next: ".js-carousel--simple-next",
    },
    // scrollLock: true,
    // scrollLockDelay: 100,
    // rewind: true,
});

const $promocaoCarousel = document.querySelector(".js-carousel--promocao");

new Glider($promocaoCarousel, {
    slidesToShow: 4,
    slidesToScroll: 1,
    draggable: true,
    //dots: ".js-carousel--simple-dots",
    arrows: {
        prev: ".js-carousel--pro-prev",
        next: ".js-carousel--pro-next",
    },
    // scrollLock: true,
    // scrollLockDelay: 100,
    // rewind: true,
});
