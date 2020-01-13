﻿$(document).ready(function () {
    $("#addInputsForIngredient").click(addInputsForIngredient);
    $("#addInputsForStep").click(addInputsForStep);

    $("#deleteIngredient").click(deleteIngredient);
    $("#deleteStep").click(deleteStep);

    $("#moveUpStep").click(moveUpStep);
    $("#moveDownStep").click(moveDownStep);
});

var ingredientNum = 0;
function addInputsForIngredient() {
    ingredientNum++;

    var ingredientDiv = $("<div>");
    ingredientDiv.attr({ class: 'ingredient' });
    ingredientDiv.attr({ id: ingredientNum.toString() });

    var labelName = $("<label>").text("Name:");
    labelName.attr({ for: 'Recipe_Ingredients_' + ingredientNum + '__Name' });

    var inputName = $("<input>");
    inputName.attr({ type: 'text' });
    inputName.attr({ id: 'Recipe_Ingredients_' + ingredientNum + '__Name' });
    inputName.attr({ name: 'Recipe.Ingredients[' + ingredientNum + '].Name' });

    var labelAmount = $("<label>").text("Amount:");
    labelAmount.attr({ for: 'Recipe_Ingredients_' + ingredientNum + '__Amount' });
    var inputAmount = $("<input>");
    inputAmount.attr({ type: 'number' });
    inputAmount.attr({ id: 'Recipe_Ingredients_' + ingredientNum + '__Amount' });
    inputAmount.attr({ name: 'Recipe.Ingredients[' + ingredientNum + '].Amount' });

    var labelMeasuringUnit = $("<label>").text("Measuring unit:");
    labelMeasuringUnit.attr({ for: 'Recipe_Ingredients_' + ingredientNum + '__MeasuringUnit' });
    var inputMeasuringUnit = $("<input>");
    inputMeasuringUnit.attr({ type: 'text' });
    inputMeasuringUnit.attr({ id: 'Recipe_Ingredients_' + ingredientNum + '__MeasuringUnit' });
    inputMeasuringUnit.attr({ name: 'Recipe.Ingredients[' + ingredientNum + '].MeasuringUnit' });

    var deleteButton = $('<button>').text("Delete ingredient");
    deleteButton.attr({ id: 'deleteIngredient' });
    deleteButton.attr({ type: 'button' });
    deleteButton.click(deleteIngredient);

    ingredientDiv.append(labelName);
    ingredientDiv.append(inputName);
    ingredientDiv.append(labelAmount);
    ingredientDiv.append(inputAmount);
    ingredientDiv.append(labelMeasuringUnit);
    ingredientDiv.append(inputMeasuringUnit);
    ingredientDiv.append(deleteButton);

    $("#Ingredients").append(ingredientDiv);
}

function deleteIngredient() {
    if (ingredientNum > 0) {
        var ingredientId = event.srcElement.parentElement.id;

        var k;
        for (var k = parseInt(ingredientId); k < $("#Ingredients").children().length - 1; k++) {
            kNext = k + 1;
            $("#Recipe_Ingredients_" + k + "__Name").val($("#Recipe_Ingredients_" + kNext + "__Name").val());
            $("#Recipe_Ingredients_" + k + "__Amount").val($("#Recipe_Ingredients_" + kNext + "__Amount").val());
            $("#Recipe_Ingredients_" + k + "__MeasuringUnit").val($("#Recipe_Ingredients_" + kNext + "__MeasuringUnit").val());
        }

        $("#Ingredients").children().last().remove();
        ingredientNum -= 1;
    } else {
        if (ingredientNum == 0) {
            $("#Recipe_Ingredients_0__Name").val("");
            $("#Recipe_Ingredients_0__Amount").val("");
            $("#Recipe_Ingredients_0__MeasuringUnit").val("");
        }
    }
}

var stepNum = 0;
function addInputsForStep() {
    stepNum++;

    var stepDiv = $("<div>");
    stepDiv.attr({ class: 'step' });
    stepDiv.attr({ id: stepNum.toString() });

    var labelOrder = $("<label>").text((stepNum + 1).toString() + ".");

    var labelDesc = $("<label>").text("Description:");
    labelDesc.attr({ for: 'Recipe_Steps_' + stepNum + '__Description' });

    var inputDesc = $("<input>");
    inputDesc.attr({ type: 'text' });
    inputDesc.attr({ id: 'Recipe_Steps_' + stepNum + '__Description' });
    inputDesc.attr({ name: 'Recipe.Steps[' + stepNum + '].Description' });

    var moveUpButton = $('<button>').text("Move up");
    moveUpButton.attr({ id: 'moveUpStep' });
    moveUpButton.attr({ type: 'button' });
    moveUpButton.click(moveUpStep);

    var moveDownButton = $('<button>').text("Move down");
    moveDownButton.attr({ id: 'moveDownStep' });
    moveDownButton.attr({ type: 'button' });
    moveDownButton.click(moveDownStep);

    var deleteButton = $('<button>').text("Delete step");
    deleteButton.attr({ id: 'deleteStep' });
    deleteButton.attr({ type: 'button' });
    deleteButton.click(deleteStep);

    stepDiv.append(labelOrder);
    stepDiv.append(labelDesc);
    stepDiv.append(inputDesc);
    stepDiv.append(moveUpButton);
    stepDiv.append(moveDownButton);
    stepDiv.append(deleteButton);

    $("#Steps").append(stepDiv);
}

function deleteStep() {
    if (stepNum > 0) {
        var stepId = event.srcElement.parentElement.id;

        for (var k = parseInt(stepId); k < $("#Steps").children().length - 1; k++) {
            kNext = k + 1;
            $("#Recipe_Steps_" + k + "__Description").val($("#Recipe_Steps_" + kNext + "__Description").val());
        }

        $("#Steps").children().last().remove();
        stepNum--;
    } else {
        if (stepNum == 0) {
            $("#Recipe_Steps_0__Description").val("");
        }
    }

}

function moveUpStep() {
    var stepId = event.srcElement.parentElement.id;
    var previousStepId = (parseInt(stepId) - 1).toString();

    if (parseInt(stepId) > 0) {
        var str = $("#Recipe_Steps_" + previousStepId + "__Description").val();
        $("#Recipe_Steps_" + previousStepId + "__Description").val($("#Recipe_Steps_" + stepId + "__Description").val());
        $("#Recipe_Steps_" + stepId + "__Description").val(str);
    }
}

function moveDownStep() {
    var stepId = event.srcElement.parentElement.id;
    var previousStepId = (parseInt(stepId) + 1).toString();

    if (stepId < stepNum) {
        var str = $("#Recipe_Steps_" + stepId + "__Description").val();
        $("#Recipe_Steps_" + stepId + "__Description").val($("#Recipe_Steps_" + previousStepId + "__Description").val());
        $("#Recipe_Steps_" + previousStepId + "__Description").val(str);
    }
}