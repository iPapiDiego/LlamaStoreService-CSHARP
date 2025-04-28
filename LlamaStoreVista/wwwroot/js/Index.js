
const carousel = document.getElementById('carousel');
const slides = carousel.querySelectorAll('.slide');
let index = 0;

function scrollCarousel(direction) {
    index += direction;
    if (index < 0) index = slides.length - 1;
    if (index >= slides.length) index = 0;

    const slideWidth = slides[0].offsetWidth + 16; // slide + gap
    carousel.scrollTo({
        left: slideWidth * index,
        behavior: 'smooth'
    });
}

setInterval(() => {
    scrollCarousel(1);
}, 3000); // cambia cada 3 segundos
