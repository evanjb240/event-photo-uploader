<template>
    <div class="party-member">
        <div class="party-member-name centered">{{ props.name }} - {{ props.position }}</div>
        <img :src="props.imageSrc"/>
    </div>
</template>
<script setup>
const props = defineProps(['name', 'position', 'imageSrc']);
</script>
<style>
.party-member-name{
    font-family: 'Angola Beach';
    font-size: 40px;
    padding-top: 10px;
    padding-bottom: 10px;
}
</style>