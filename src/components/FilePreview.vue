<template>
    <component :is="tag" >
        <div class="container">
            <img class="file-preview" :src="file.url" :alt="file.file.name" :title="file.file.name" />
            <button @click.stop="$emit('cancel', file)" class="close-icon btn" aria-label="Remove">X</button>
        </div>
    </component>
</template>

<script setup>
defineEmits(['cancel'])
defineProps({
    file: { type: Object, required: true },
    tag: { type: String, default: 'li' },
})
</script>

<style scoped>
.container {
  position: relative;
  width: 100%;
  max-width: 400px;
}

.container .btn {
  position: absolute;
  top: 5%;
  left: 5%;
  transform: translate(-50%, -50%);
  -ms-transform: translate(-50%, -50%);
  background-color: #555;
  color: white;
  font-size: 12px;
  padding: 8px 12px;
  border: none;
  cursor: pointer;
  border-radius: 5px;
  text-align: center;
}

.container .btn:hover {
  background-color: black;
}

.file-preview {
  width: 150px;
  border-radius: 10px;
  overflow: hidden;
  background-color: #cccccc;
  background-size: cover;
  position: relative;
}
.file-preview::after {
  content: attr(data-label);
  position: absolute;
  bottom: 0;
  left: 0;
  width: 100%;
  padding: 5px 0;
  color: #ffffff;
  background: rgba(0, 0, 0, 0.75);
  font-size: 14px;
  text-align: center;
}
</style>