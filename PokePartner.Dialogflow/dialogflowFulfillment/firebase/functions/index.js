// See https://github.com/dialogflow/dialogflow-fulfillment-nodejs
// for Dialogflow fulfillment library docs, samples, and to report issues
'use strict';

const axios = require('axios');
const address = '';

const functions = require('firebase-functions');
const {WebhookClient} = require('dialogflow-fulfillment');
const {Card, Suggestion} = require('dialogflow-fulfillment');
 
process.env.DEBUG = 'dialogflow:debug'; // enables lib debugging statements
 
exports.dialogflowFirebaseFulfillment = functions.https.onRequest((request, response) => {
  const agent = new WebhookClient({ request, response });
  console.log('Dialogflow Request headers: ' + JSON.stringify(request.headers));
  console.log('Dialogflow Request body: ' + JSON.stringify(request.body));
 
  function welcome(agent) {
    agent.add(`Welcome to my agent!`);
  }
 
  function fallback(agent) {
    agent.add(`I didn't understand`);
    agent.add(`I'm sorry, can you try again?`);
  }
 
  process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0'; // Remove this later, need to troubleshoot my domain/https/SSL.
  
  // Single Type 
  function singleTypeHandler(agent){
    const type = agent.parameters.type.toLowerCase();  
    return axios.get(`/type/defense/${type}`)
    .then((result) => {   
      agent.add(`${type} Type Pokémon are weak to ${result.data.superEffective}. So if you want to do the most damage, use one of those types.`); 
      if (result.data.notVeryEffective.length == 0 && result.data.noEffect.length == 0) {
        // If pokemon has notVeryEffect and NoEffect
      } else if (result.data.noEffect.length == 0) {
        // If pokemon has Weakness but no NoEffects
        agent.add(` Do not use ${result.data.notVeryEffective} As those types are not very effective against ${type} Type Pokemon.`);
      } else {
        // If Pokemon has NoEffect only
        agent.add(`By the way, don't use ${result.data.noEffect} because it will have no effect.`);
      }
    }); 
  }
  
  // Dual Type 
  function dualTypeHandler(agent){
    const type1 = agent.parameters.type1.toLowerCase(); 
    const type2 = agent.parameters.type2.toLowerCase();
    
    return axios.get(`${address}/type/defense/${type1}/${type2}`)
    .then((result) => {

      if (result.data.superEffectiveX4.length > 0 && result.data.superEffectiveX2.length > 0) {
        agent.add(`${type1}/${type2} Type Pokémon are weak times four to ${result.data.superEffectiveX4}. So if you want to do the most damage, use one of those types. You can also use ${result.data.superEffectiveX2} attacks, which ${type1}/${type2} Pokemon are weak times two against.`);
      } else if (result.data.superEffectiveX4.length > 0) {
        agent.add(`${type1}/${type2} Type Pokémon are weak times four to ${result.data.superEffectiveX4}. So if you want to do the most damage, use one of those types.`);
      } else if (result.data.superEffectiveX2.length > 0) {
        agent.add(`${type1}/${type2} Type Pokémon are weak times two to ${result.data.superEffectiveX2}. So if you want to do the most damage, use one of those types.`);
      } else {
        // do nothing
      }
      if (result.data.notVeryEffectiveHalf.length > 0 && result.data.notVeryEffectiveQuarter.length > 0 && result.data.NoEffect.length > 0) {
        agent.add(`${type1}/${type2} Type Pokémon only take half damage from ${result.data.notVeryEffectiveHalf} attacks and only quarter damage from ${result.data.notVeryEffectiveQuarter} attacks, so don't use those. Also ${result.data.NoEffect} will have no effect.`);
      } 
      else if (result.data.notVeryEffectiveHalf.length > 0 && result.data.NoEffect.length > 0) {
        agent.add(`${type1}/${type2} Type Pokémon only take half damage from ${result.data.notVeryEffectiveHalf}, so don't use those types of attacks. Also ${result.data.NoEffect} will have no effect.`);
      } 
      else if (result.data.notVeryEffectiveQuarter.length > 0 && result.data.NoEffect.length > 0) {
        agent.add(`${type1}/${type2} Type Pokémon only take quarter damage from ${result.data.notVeryEffectiveQuarter} attacks. Do not use these types of attacks. Also ${result.data.NoEffect} will have no effect.`);
      } 
      else if (result.data.notVeryEffectiveHalf.length > 0) {
        agent.add(`${type1}/${type2} Type Pokémon only take half damage from ${result.data.notVeryEffectiveHalf}, so don't use those types of attacks.`);
      }  
      else if (result.data.notVeryEffectiveQuarter.length > 0) {
        agent.add(`${type1}/${type2} Type Pokémon only take quarter damage from ${result.data.notVeryEffectiveQuarter} attacks. Do not use these types of attacks.`);
      } 
      else {
        // do nothing
      }
    });
  }
  
  // Pokemon Weakness
  function pokemonWeaknessHandler(agent){
    const pokemonName = agent.parameters.pokemonName.toLowerCase(); 
    return axios.get(`${address}/type/pokemon/${pokemonName}`)
    .then((result) => {   
      if (result.data.superEffectiveX4.length > 0 && result.data.superEffectiveX2.length > 0) {
        agent.add(`${pokemonName} is weak times four to ${result.data.superEffectiveX4}. So if you want to do the most damage, use one of those types. You can also use ${result.data.superEffectiveX2} attacks, which ${pokemonName} is weak times two against.`);
      } else if (result.data.superEffectiveX4.length > 0) {
        agent.add(`${pokemonName} is weak times four to ${result.data.superEffectiveX4}. So if you want to do the most damage, use one of those types.`);
      } else if (result.data.superEffectiveX2.length > 0) {
        agent.add(`${pokemonName} is weak times two to ${result.data.superEffectiveX2}. So if you want to do the most damage, use one of those types.`);
      } else {
        // do nothing
      }
      if (result.data.notVeryEffectiveHalf.length > 0 && result.data.notVeryEffectiveQuarter.length > 0 && result.data.NoEffect.length > 0) {
        agent.add(`${pokemonName} will only take half damage from ${result.data.notVeryEffectiveHalf} attacks and only quarter damage from ${result.data.notVeryEffectiveQuarter} attacks, so don't use those. Also ${result.data.NoEffect} will have no effect.`);
      } 
      else if (result.data.notVeryEffectiveHalf.length > 0 && result.data.NoEffect.length > 0) {
        agent.add(`${pokemonName} will only take half damage from ${result.data.notVeryEffectiveHalf}, so don't use those types of attacks. Also ${result.data.NoEffect} will have no effect.`);
      } 
      else if (result.data.notVeryEffectiveQuarter.length > 0 && result.data.NoEffect.length > 0) {
        agent.add(`${pokemonName} will only take quarter damage from ${result.data.notVeryEffectiveQuarter} attacks. Do not use these types of attacks. Also ${result.data.NoEffect} will have no effect.`);
      } 
      else if (result.data.notVeryEffectiveHalf.length > 0) {
        agent.add(`${pokemonName} will only take half damage from ${result.data.notVeryEffectiveHalf}, so don't use those types of attacks.`);
      }  
      else if (result.data.notVeryEffectiveQuarter.length > 0) {
        agent.add(`${pokemonName} will only take quarter damage from ${result.data.notVeryEffectiveQuarter} attacks. Do not use these types of attacks.`);
      } 
      else {
        // do nothing
      }
      
      
    }); 
  }

  // Run the proper function handler based on the matched Dialogflow intent name
  let intentMap = new Map();
  intentMap.set('Default Welcome Intent', welcome);
  intentMap.set('Default Fallback Intent', fallback);
  intentMap.set('SingleType', singleTypeHandler);
  intentMap.set('DualType', dualTypeHandler);
  intentMap.set('PokemonWeakness', pokemonWeaknessHandler);
  agent.handleRequest(intentMap);
});
