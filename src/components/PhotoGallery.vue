<template>
  <div class="image-list" v-show="images">
    <div class="image-item" v-for="file of images" :key="file.name">
      <img :src="file.uri" class="" />
    </div>
  </div>
</template>

<script setup>

defineProps({
  images: Array
});

</script>

<style scoped>
.image-list {
  margin: 20px;
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  column-gap: 20px;
}

.image-item {
  margin-top: 15px;
  overflow: hidden;
  background-size: cover;
  padding: 3px;
  border-radius: 4px;
  border: 2px solid #e7e7e7;
  box-shadow: 0px 3px 6px #000000a8;
}

.image-item img {
  width: 100%;
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
}
</style>