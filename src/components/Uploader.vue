
<template>
    <div class="container">
        <div class="row">
            <div class="drop-zone col-md-4 offset-md-4">
                <span class="drop-zone__prompt">Click to take a picture!</span>
                <input id="file-input" type="file" name="myFile" class="drop-zone__input" capture="environment">
            </div>
        </div>
        <div class="row">
            <div class="col-md-4"></div>
            <button id="upload-button" class="upload-button primary-button col-md-4">Upload!</button>
            <div class="col-md-4"></div>
        </div>
    </div>
</template>