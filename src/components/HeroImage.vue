<template>
    <div class="hero-image">
        <div v-if="props.text && !props.link" class="hero-text">
            <h1>{{ text }}</h1>
        </div>
        <div v-if="props.text && props.link" class="hero-text">
            <NuxtLink class="hero-action" :to="props.link">{{ props.text }}</NuxtLink>
        </div>
    </div>
</template>
<script setup>

const props = defineProps({
    image: { type: String, default: '' },
    text:{ type:String, default: ''},
    link: { type:String, default: ''},
    height: { type:Number, default: 350 }
})
</script>
<style scoped>
.hero-image {
  background: v-bind("props.image") no-repeat center center; 
  background-size: cover;
  height:350px;
  background-blend-mode: lighten;
  display:flex;
  align-items:center;
  flex-direction: column;
}

.hero-text {
  text-align: center;
  position: absolute;
  color: white;
  font-size: 40px;
  font-family: 'Baliva';
  margin-top:50px;
}

.hero-text .hero-action {
  border: 2px solid white;
  background: rgba(10, 10, 10, 0.5);
  outline: 1;
  display: inline-block;
  padding: 10px 25px;
  color: white;
  text-align: center;
  cursor: pointer;
}

.hero-text .hero-action:hover {
  background-color: #555;
  color: white;
}
</style>