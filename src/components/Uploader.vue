<template>
  <DropZone class="drop-zone" @files-dropped="addFiles" #default="{ dropZoneActive }">
    <label class="drop-zone__prompt" for="file-input">
      <span v-if="dropZoneActive">
        <span>Drop Them Here </span>
        <span class="smaller">to add them</span>
      </span>
      <span v-else>
        <span>Drag Your Files Here</span>
        <span class="smaller">
          or <strong><em>click here</em></strong> to select files
        </span>
      </span>

      <input class="drop-zone__input" type="file" id="file-input" @change="onInputChange" />
    </label>
    <ul class="image-list" v-show="files.length">
      <FilePreview v-for="file of files" :key="file.id" :file="file" tag="li" @cancel="removeFile" />
    </ul>
  </DropZone>
  <div>
    <label v-if="true">{{ uploadResultMessage }}</label>
  </div>  
  <div>
    <button class="upload-button" @click="upload">Upload</button>
  </div>
  <PhotoGallery :images="photoGallery.files"></PhotoGallery>
</template>

<script setup>
import useFileList from '../compositions/file-list.js'
import DropZone from './DropZone.vue'
import FilePreview from './FilePreview.vue'
import PhotoGallery from './PhotoGallery.vue';
import { ref } from 'vue';

const { files, addFiles, removeFile, removeFiles } = useFileList()
const uploadResult = ref(true);
const uploadResultMessage= ref('');
const photoGallery = ref([]);

getUploads();


function onInputChange(e) {
  addFiles(e.target.files)
  e.target.value = null
}

function upload() {
  var data = new FormData();

  data.append('file', files.value[0].file);

  fetch('/api/Uploader', {
    method: 'POST',
    body: data
  }).then((response) => response.json())
  .then((data) => {
    uploadResult.value = true;
    uploadResultMessage.value = data.message;
    removeFiles();
    getUploads();
  })
  .catch((error) => {
    uploadResult.value = true;
    uploadResultMessage.value = error
  });
}

function getUploads() {
  fetch('/api/BlobLister', {
    method: 'GET',
  }).then((response) => response.json())
  .then((data) => {
    photoGallery.value = data;
  })
  .catch((error) => {
  });
}
</script>

<style>
.drop-zone {
  margin: 50px;
  background-color: #ffffff;
  min-width: 200px;
  height: 200px;
  padding: 25px;
  display: flex;
  align-items: center;
  justify-content: center;
  text-align: center;
  font-family: "Quicksand", sans-serif;
  font-weight: 500;
  font-size: 20px;
  cursor: pointer;
  color: #cccccc;
  border: 4px dashed #7082D0;
  border-radius: 10px;
}

.drop-zone--over {
  border-style: solid;
}

.drop-zone__input {
  display: none;
}

.drop-zone__prompt {
  color: var(--primary-color);
}

ul {
  list-style-type: none;
}

.upload-button {
  margin-top: 25px;
  margin-bottom: 25px;
}
</style>