<template>
    <DropZone class="drop-zone" @files-dropped="addFiles" #default="{ dropZoneActive }">
      <label class="drop-zone__prompt" for="file-input">
        <span class="drop-zone__span" v-if="dropZoneActive">
          <span>Drop Them Here </span>
          <span class="smaller">to add them</span>
        </span>
        <span class="drop-zone__span" v-else>
          <span>Drag Your Files Here</span>
          <span class="smaller">
            or <strong><em>click here</em></strong> to select files
          </span>
        </span>
      </label>
      <input class="drop-zone__input" type="file" accept="image/*" id="file-input" multiple @change="onInputChange" />
      <div class="image-list" v-show="files.length">
        <FilePreview v-for="file of files" :key="file.id" :file="file" tag="div" @cancel="removeFile" />
      </div>
    </DropZone>
    <div>
      <FormMessage v-if="uploadResult" :message="uploadResultMessage" :success-state="successState"></FormMessage>
    </div>
    <div class="button-container">
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
const uploadResultMessage = ref('');
const successState = ref('');
const photoGallery = ref([]);

getUploads();


function onInputChange(e) {
  addFiles(e.target.files)
  e.target.value = null
  uploadResult.value = false;
  uploadResultMessage.value = "";
  successState.value = "";
}

function upload() {

  if (files.value.length == 0) {
    uploadResult.value = true;
    uploadResultMessage.value = "Attach a file!";
    successState.value = "error";
    return;
  }

  files.value.forEach((file)=> {
    var data = new FormData();

    data.append('file', file.file);
    
    fetch('/api/Uploader', {
      method: 'POST',
      body: data
    }).then((response) => response.json())
      .then((data) => {
        uploadResult.value = true;
        uploadResultMessage.value = data.message;
        successState.value = 'success';
      })
      .catch((error) => {
        uploadResult.value = true;
        uploadResultMessage.value = error
      });
  })

  removeFiles();
  getUploads();
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

<style scoped>

.drop-zone {
  margin: 20px;
  background-color: #ffffff;
  min-width: 200px;
  min-height: 200px;
  padding: 25px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  text-align: center;
  font-family: "Quicksand", sans-serif;
  font-weight: 500;
  font-size: 20px;
  color: #cccccc;
  box-shadow: 0px 3px 6px #000000a8;
  opacity: 1;
}

.drop-zone--over {
  border-style: solid;
}

.drop-zone__input {
  display: none;
}



.drop-zone__prompt {
  width:100%;
  min-height: 200px;
  display: block;
  text-align: center;
  color: #807e7e;
  cursor: pointer;
}

.button-container {
  margin: 5px auto;
  width: 300px;
  text-align: center;
}

.upload-button {
  font-size: 1rem;
  font-weight: 500;
  box-shadow: 0px 3px 6px #000000a8;
  width: 150px;
  height: 50px;
  margin-top: 25px;
  margin-bottom: 50px;
}

.drop-zone-stack{
  display:flex;
  flex-direction: column;
}
.image-list{
  display: flex;
  gap:10px;
  flex-flow:wrap;
  justify-content: center;
}
</style>