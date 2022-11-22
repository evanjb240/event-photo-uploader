<template>
    <div v-if="successState == 'info'" class="info-msg">
        <i class="fa fa-info-circle"></i>
        {{ message }}
    </div>

    <div v-if="successState == 'success'" class="success-msg">
        <i class="fa fa-check"></i>
        {{ message }}
    </div>

    <div v-if="successState == 'warning'" class="warning-msg">
        <i class="fa fa-warning"></i>
        {{ message }}
    </div>

    <div v-if="successState == 'error'" class="error-msg">
        <i class="fa fa-times-circle"></i>
        {{ message }}
    </div>
</template>

<script setup>
defineProps({
    successState: { type: String, default: '' },
    message: { type: String, default: '' },
})
</script>

<style scoped>
@import url('//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css');

.info-msg,
.success-msg,
.warning-msg,
.error-msg {
    width: 60%;
    margin: 10px auto;
    padding: 10px;
    border-radius: 3px 3px 3px 3px;
}

.info-msg {
    color: #059;
    background-color: #BEF;
}

.success-msg {
    color: #270;
    background-color: #DFF2BF;
}

.warning-msg {
    color: #9F6000;
    background-color: #FEEFB3;
}

.error-msg {
    color: #D8000C;
    background-color: #FFBABA;
}
</style>