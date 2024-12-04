function NavBar() {
    var x = document.getElementById("myTopnav");
    if (x.className === "topnav") {
        x.className += " responsive";
    } else {
        x.className = "topnav";
    }
}

document.addEventListener("DOMContentLoaded", function () {
    // Back to Top button functionality
    const rollBack = document.getElementById("roll_back");
    if (rollBack) {
        rollBack.addEventListener("click", function () {
            window.scrollTo({ top: 0, behavior: 'smooth' });
        });
    }
    window.onscroll = function () { scrollFunction() };

    function scrollFunction() {
        if (document.body.scrollTop > 80 || document.documentElement.scrollTop > 80) {
            const myTopnav = document.getElementById("myTopnav");
            const header = document.getElementById("header");

            if (myTopnav) {
                myTopnav.style.width = "100%";
            }

            if (header) {
                header.style.position = "fixed";
                header.style.top = "0%";
            }

            var rollBack = document.getElementById("roll_back");
            if (rollBack) {
                rollBack.style.display = "block";
            }
        } else {
            const myTopnav = document.getElementById("myTopnav");
            const header = document.getElementById("header");

            if (myTopnav) {
                myTopnav.style.width = "80%";
            }

            if (header) {
                header.style.position = "fixed";
                header.style.top = "2rem";
            }

            var rollBack = document.getElementById("roll_back");
            if (rollBack) {
                rollBack.style.display = "none";
            }
        }
    }

    // Initial call to showSlides
    showSlides();
});

let currentSlideIndex = 0;

// Function to show slides
function showSlides() {
    let slides = document.getElementsByClassName("slide");

    if (!slides || slides.length === 0) {
        console.warn("No slides found. Skipping slideshow setup.");
        return;
    }

    // Hide all slides
    for (let i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }

    // Increment slide index
    currentSlideIndex++;

    // If at end, loop back to first slide
    if (currentSlideIndex > slides.length) {
        currentSlideIndex = 1;
    }

    // Display the current slide
    slides[currentSlideIndex - 1].style.display = "block";

    // Change slide every 3 seconds
    setTimeout(showSlides, 3000);
}

// Function to manually change slides with buttons
function changeSlide(n) {
    let slides = document.getElementsByClassName("slide");

    if (!slides || slides.length === 0) {
        console.error("No slides found.");
        return;
    }

    currentSlideIndex += n;

    // If we exceed slide length, start from the beginning
    if (currentSlideIndex > slides.length) {
        currentSlideIndex = 1;
    }

    // If below first slide, go to the last one
    if (currentSlideIndex < 1) {
        currentSlideIndex = slides.length;
    }

    // Hide all slides
    for (let i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }

    // Display the current slide
    slides[currentSlideIndex - 1].style.display = "block";
}

document.addEventListener("DOMContentLoaded", function () {
    const image = document.getElementById("parallaxImage");

    if (!image) {
        console.warn("Parallax image not found. Skipping parallax effect setup.");
        return;
    }

    // Create an Intersection Observer instance
    const observer = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                // Trigger animation when image is in view
                image.style.left = "0";
                // Unobserve once the animation is triggered
                observer.unobserve(entry.target);
            }
        });
    });

    // Observe the image element
    observer.observe(image);
});

