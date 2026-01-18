<script setup lang="ts">
  import {computed, reactive, ref, warn, watch} from 'vue';

  var clickedCount = ref(0);
  
  var h1Html ={
    style:{
      color:'',
      isValid:function isValid(){
        return this.color!='';
      }
    },
    isValid:function isValid(){
      return this.style.isValid()
    }
  }

  var jsValue=ref('');
  var watchedV = reactive({value: 0,otherThing:0});
  var theDouble = computed(()=>watchedV.value * 2);

  function onClicked(){
    clickedCount.value+=1;
    h1Html.style.color = 'rgb('+Array.from([0,0,0],()=>Math.floor(Math.random() * 255)).join(',')+')'
    console.debug('clicked ' +clickedCount.value + ' times')
  }

  watch(()=>watchedV.value,()=>{alert('xd?')})
</script>

<template>
  <h1>Playground</h1>
  <p>
    <button @click="onClicked" position="flex">just a button</button>
    <p position="flex">Clicked count: {{ clickedCount }}</p>
    <p position="flex" style="font-weight:bold; font-size:60px">
      This is <span :style="h1Html.style">XDDD</span>
    </p>
    <p id="boomParagraph" v-if="clickedCount >=5">
      BOOOOM
    </p> 
    
    <input @keyup="(event)=>{console.log(jsValue);}" type="checkbox" v-model="jsValue"></input>
    <textarea @keyup="(event)=>{console.log(jsValue);}" v-model.number='jsValue'></textarea>
    <p @click="()=>{watchedV.value+=2;console.log(watchedV);}">
      {{ watchedV }}
      <!-- {{ theDouble }} -->
    </p>
    <template >
    </template>
  </p>
</template>

<style scoped>
  #boomParagraph{
    font-size: 50px;
    border-radius:25%;
    color: #F30;
  }
</style>
