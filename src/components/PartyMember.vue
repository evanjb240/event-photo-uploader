<template>
    <div class="party-member">
        <h4>{{ props.name }}</h4>
        <div>{{ props.position }}</div>
        <img :src="props.imageSrc"/>
    </div>
</template>
<script setup>
const props = defineProps(['name', 'position', 'imageSrc']);
</script>