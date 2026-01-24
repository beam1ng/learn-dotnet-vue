<script lang="ts" setup>
import Header from './Header.vue';
import Footer from './Footer.vue'
import Wordle from './Wordle.vue';
import { ref } from 'vue';

var wordleEnabled = ref(true);

function OpenTarget(targetName:string){
  console.log('xdddd');
  
  if(targetName=='wordle')
    wordleEnabled.value = true;
}
</script>

<template>
  <div id="overlay" v-if="wordleEnabled"></div>
  <div class="page">
    <Header @open="(targetName: string)=>{OpenTarget(targetName)}" />

    <div class="content">
      <Wordle v-if="wordleEnabled" @close-modal="()=>{wordleEnabled=false;}"/>
    </div>

    <Footer />
  </div>
</template>

<style>
#overlay{
  position:fixed;
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 900;
  width: 100vw;
  height: 100vh;
}

.separator {
  flex: 1;
}

.page {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  background-color: aliceblue;
}

.content {
  flex-grow: 1;
  display:flex;
  justify-content:center;
  align-items:center;
}
</style>
