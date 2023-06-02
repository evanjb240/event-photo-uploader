<template>
    <div class="centered">
        <img src="@/public/images/WeddingLogo.png" :width="props.width" :height="props.height" />
    </div>
</template>
<script setup>
const props = defineProps({
    width:{
        type: Number,
        default: 250
    },
    height:{
        type: Number,
        default: 250
    }
});
</script>