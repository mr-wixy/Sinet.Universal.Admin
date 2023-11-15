window.app = {
    loadingStart: () => {
        document.getElementById("app").classList.add("loader")
    },
    loadingStop: () => {
        document.getElementById("app").classList.remove("loader")
        document.body.classList.remove('preloadbody');
    }
}