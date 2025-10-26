// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.querySelectorAll(".notification-btn").forEach(btn => {
  btn.addEventListener("click", function () {
    document.querySelectorAll(".notification-btn").forEach(b => {
      b.classList.remove("active");
      const img = b.querySelector('img.icon');
      if (img && img.dataset.src) img.src = img.dataset.src; // volver al color normal
    });

    this.classList.add("active");
    const imgAct = this.querySelector('img.icon');
    if (imgAct && imgAct.dataset.srcWhite) imgAct.src = imgAct.dataset.srcWhite; // poner blanco
  });
});
