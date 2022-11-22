<template>
  <div class="image-list" v-show="images">
    <img class="image-item" v-for="file of images" :key="file.name" :src="file.uri" />
  </div>
</template>

<script setup>

defineProps({
  images: Array
});

</script>

<style scoped>
.image-list {
  margin: 5px auto;
  width: 250px;
}

.image-item {
  margin: 5 auto;
  max-width: 250px;
  border-radius: 10px;
  overflow: hidden;
  background-color: #cccccc;
  background-size: cover;
  position: relative;
}
</style>