function hideRevealButton(button) {
    let lastScroll = 0;

    window.addEventListener("scroll", () => {
        const currentScroll = document.documentElement.scrollTop;
        const windowScroll = document.documentElement.clientHeight;
        const heightScroll = document.documentElement.scrollHeight;

        if (currentScroll > lastScroll) {
            // down
            button.classList.remove("slide-up");

            if (heightScroll - currentScroll <= windowScroll) {
                button.classList.remove("sticky");
            }

            else {
                button.style.bottom = -currentScroll / 3 + "px";
            }

        }
        else if (currentScroll < lastScroll) {
            // up
            button.style.bottom = "45px";

            button.classList.add("sticky");

            button.classList.add("slide-up");
        }

        lastScroll = currentScroll;
    });

    
}
