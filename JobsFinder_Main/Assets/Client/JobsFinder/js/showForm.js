var buttons = document.querySelectorAll(".btn-show-form")
var boxForm = document.getElementById("form-input")
var overlay = document.getElementById("overlay")

for (let i = 0; i < buttons.length; i++) {
    buttons[i].addEventListener("click", function () {
        boxForm.classList.add("show-form-input")
        overlay.classList.add("show-overlay")

        var formID = this.getAttribute("data-form")
        var form = document.getElementById(formID)
        form.classList.add("show-form-profile")
        window.scrollTo({
            top: 0,
            behavior: "smooth"
        })
    })
}

overlay.addEventListener("click", function () {
    boxForm.classList.remove("show-form-input");
    overlay.classList.remove("show-overlay");

    var forms = document.querySelectorAll(".show-form-profile");
    for (let j = 0; j < forms.length; j++) {
        forms[j].classList.remove("show-form-profile");
    }
});
