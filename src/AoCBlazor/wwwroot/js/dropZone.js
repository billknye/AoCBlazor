export function initializeFileDropZone(inputFile) {
    // Add a class when the user drags a file over the drop zone
    function onDragHover(e) {
        e.preventDefault();
        //window.classList.add("hover");
    }

    function onDragLeave(e) {
        e.preventDefault();
        //dropZoneElement.classList.remove("hover");
    }

    // Handle the paste and drop events
    function onDrop(e) {
        e.preventDefault();
        //dropZoneElement.classList.remove("hover");

        // Set the files property of the input element and raise the change event
        inputFile.files = e.dataTransfer.files;

        const event = new Event('change', { bubbles: true });
        inputFile.dispatchEvent(event);
    }

    function onPaste(e) {
        console.log('paste', e);

        if (e.clipboardData.files.length) {
            // Set the files property of the input element and raise the change event
            inputFile.files = e.clipboardData.files;
            const event = new Event('change', { bubbles: true });
            inputFile.dispatchEvent(event);
        }
        else {
            var text = (event.clipboardData || window.clipboardData).getData('text');

            if (text) {
                if (/\r|\n/.exec(text)) {
                    
                    // Set the files property of the input element and raise the change event
                    var files = new DataTransfer();
                    var file = new File([text], "pastedText.txt");
                    files.items.add(file);

                    inputFile.files = files.files;
                    const event = new Event('change', { bubbles: true });
                    inputFile.dispatchEvent(event);

                    // stop paste from affecting elsewhere
                    e.preventDefault();
                }
                else {
                    // single line, ignore
                }
            }
        }        
    }

    // Register all events
    window.addEventListener("dragenter", onDragHover);
    window.addEventListener("dragover", onDragHover);
    window.addEventListener("dragleave", onDragLeave);
    window.addEventListener("drop", onDrop);
    window.addEventListener('paste', onPaste);

    // The returned object allows to unregister the events when the Blazor component is destroyed
    return {
        dispose: () => {
            window.removeEventListener('dragenter', onDragHover);
            window.removeEventListener('dragover', onDragHover);
            window.removeEventListener('dragleave', onDragLeave);
            window.removeEventListener("drop", onDrop);
            window.removeEventListener('paste', onPaste);
        }
    }
}