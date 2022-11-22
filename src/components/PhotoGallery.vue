<template>
    <div class="image-list" v-show="images">
        <img class="image-item" v-for="file of images" :key="file.name" :src="file.uri" />
    </div>
</template>

<script setup>

defineProps({
  images:Array
});

</script>

<style scoped>
.image-item {
  max-width: 250px;
  border-radius: 10px;
  overflow: hidden;
  background-color: #cccccc;
  background-size: cover;
  position: relative;
}
</style>