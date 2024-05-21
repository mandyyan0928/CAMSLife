function isEmpty(input) {
    if (input === undefined)
        return true;
    if (input === '')
        return true;
    if (input.length === 0)
        return true;
    if (input === null)
        return true;

   

    

    return false
}

function formatInputToFloat(input) {
    if (isEmpty(input))
        return 0;
    if (isNaN(input))
        return 0;
    else
        return parseFloat(input);
}

function formatCommaTextToFloat(input) {
    if (isEmpty(input))
        return 0;
    else
        return parseFloat(input.replaceAll(',', ''));
}
