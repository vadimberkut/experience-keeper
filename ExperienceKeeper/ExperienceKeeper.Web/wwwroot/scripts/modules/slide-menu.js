


let slideMenu = document.querySelector('.slide-menu');
let displayControl = slideMenu.querySelector('.slide-menu__display-control');
let menuContent = slideMenu.querySelector('.slide-menu__content');
let overlay = slideMenu.querySelector('.slide-menu__bg-overlay');

const transitionTime = 300;
const transitionDelay = 100;
let transitionTimeout = null;

displayControl.addEventListener('click', (e) => {
    displayMenu();
});
overlay.addEventListener('click', (e) => {
    hideMenu();
});

function displayMenu() {
    slideMenu.classList.add('active');
}
function hideMenu() {
    slideMenu.classList.remove('active');
}