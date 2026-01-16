<script setup lang="ts">
  import {ref} from 'vue';
  // export default{
  //   setup() {
  //     return {
  //       clickedCount,
  //       r,g,b,onClicked
  //     }
  //   }
  // }

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
  
  var r = 250;
  var g = 100;
  var b= 50;

  function onClicked(){
    clickedCount.value+=1;
    h1Html.style.color = 'rgb('+Array.from([0,0,0],()=>Math.floor(Math.random() * 255)).join(',')+')'
    console.debug('clicked ' +clickedCount.value + ' times')
  }
</script>

<template>
  <h1>Playground</h1>
  <p>
    <button @click="onClicked" position="flex">just a button</button>
    <p position="flex">Clicked count: {{ clickedCount }}</p>
    <p position="flex" style="font-weight:bold; font-size:60px">
      This is <span :style="`color:rgb(${r},${g},${b})`">XDDD</span>
    </p>
    <p id="boomParagraph" v-if="clickedCount >=5">
      BOOOOM
    </p> 
  </p>
</template>

<style scoped>
  #boomParagraph{
    font-size: 50px;
    border-radius:25%;
    color: #F30;
  }
</style>
