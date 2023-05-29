<template>
    <div class="hero-image">
        <div class="hero-text">
            <h1>{{ text }}</h1>
        </div>
    </div>
</template>
<script setup>

const props = defineProps({
    image: { type: String, required: true, default: '' },
    text:{ type:String, required:false, default: ''}
})
</script>
<style scoped>
.hero-image {
  background: v-bind("props.image") no-repeat center center; 
  background-size: cover;
  height:350px;
  background-blend-mode: lighten;
  display:flex;
  flex-direction: column;
}

.hero-text {
  text-align: center;
  position: absolute;
  color: white;
  font-size: 40px;
  font-family: 'Baliva';
}

.hero-text button {
  border: none;
  outline: 0;
  display: inline-block;
  padding: 10px 25px;
  color: black;
  background-color: #ddd;
  text-align: center;
  cursor: pointer;
}

.hero-text button:hover {
  background-color: #555;
  color: white;
}
</style>