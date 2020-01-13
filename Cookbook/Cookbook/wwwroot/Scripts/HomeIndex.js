$(document).ready(function () {
    $(".recipe").on("click", function (e) {
        displayRecipe(e.target.id);
    });
});

function displayRecipe(recipeId) {
    window.location.href = "/Recipe/" + recipeId;
}