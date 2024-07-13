function copyToClipboard(text) {
    return navigator.clipboard.writeText(text).then(function() {
        console.log('Copying to clipboard was successful!');
        return true;
    }, function(err) {
        console.error('Could not copy text: ', err);
        return false;
    });
}