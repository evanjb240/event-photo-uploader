<template>
    <VueDatePicker :model-value="date" 
      @update:model-value="handleDate" 
      :format="format" 
      model-type="MM/dd/yyyy" 
      :enable-time-picker="false"
      auto-apply
      ></VueDatePicker>
  </template>
  
  <script setup>
  import { ref } from 'vue';
  import VueDatePicker from '@vuepic/vue-datepicker';
  import '@vuepic/vue-datepicker/dist/main.css'
  
  const props = defineProps({
    date: {
        type: String,
        default: new Date()
    }
  })
  const emit = defineEmits('update:date');

  const date = ref(props.date);

  const handleDate = (modelData) => {
    date.value = modelData;
    emit('update:date', date.value);
  }

  const format = (date) => {
    const day = date.getDate();
    const month = date.getMonth() + 1;
    const year = date.getFullYear();

    return `${month}/${day}/${year}`;
  }

  </script>