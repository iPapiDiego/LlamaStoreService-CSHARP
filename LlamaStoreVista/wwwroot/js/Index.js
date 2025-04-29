const track = document.getElementById("carousel-track");
const cards = document.querySelectorAll(".card");
let index = 0;

setInterval(() => {
    index = (index + 1) % cards.length;
    track.style.transform = `translateX(-${index * cards[0].offsetWidth}px)`;
}, 4000);