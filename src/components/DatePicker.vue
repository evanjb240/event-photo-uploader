<template>
    <VueDatePicker v-model="date" @update:model-value="handleDate"></VueDatePicker>
  </template>
  
  <script setup>
  import { ref } from 'vue';
  import VueDatePicker from '@vuepic/vue-datepicker';
  import '@vuepic/vue-datepicker/dist/main.css'
  
  const props = defineProps({
    date: {
        type: Date,
        default: new Date()
    }
  })
  const emit = defineEmits('update:date');

  const date = ref(props.date);

  const handleDate = (modelData) => {
    date.value = modelData;
    emit('update:date', date.value);
  }

  </script>